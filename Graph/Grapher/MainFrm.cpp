// MainFrm.cpp : implementation of the CMainFrame class
//

#include "stdafx.h"
#include "MultTopLevel.h"
#include "GraphDialog.h"
#include "MainFrm.h"
#include "Tracer.h"
#include <fstream>


#ifdef _DEBUG
#define new DEBUG_NEW
#endif
const char CMainFrame::s_Heading[] = "Current preferences";
const char CMainFrame::s_Scale[] = "Current scale";
const char CMainFrame::s_Grid[] = "Grid state";
const char CMainFrame::s_File[] = "Current file";

// CMainFrame

IMPLEMENT_DYNAMIC(CMainFrame, CFrameWnd)

BEGIN_MESSAGE_MAP(CMainFrame, CFrameWnd)
	ON_WM_CREATE()
	ON_COMMAND(ID_FILE_CLOSE, &CMainFrame::OnFileClose)
	ON_WM_CLOSE()
	ON_WM_LBUTTONDOWN()
	ON_WM_SIZE()
	ON_WM_RBUTTONDOWN()
	ON_COMMAND(ID_GRAPHIC, &CMainFrame::OnGraphic)
	ON_WM_PAINT()
	ON_COMMAND(ID_PREFERENCES, &CMainFrame::OnPreferences)
	ON_COMMAND(ID_TOOLS_MATH, &CMainFrame::OnToolsMath)
	ON_WM_DESTROY()
	ON_COMMAND(ID_FILE_OPEN, &CMainFrame::OnFileOpen)
	ON_COMMAND(ID_FILE_SAVE32789, &CMainFrame::OnFileSave)
	ON_COMMAND(ID_TOOLS_TRACE, &CMainFrame::OnToolsTrace)
END_MESSAGE_MAP()

static UINT indicators[] =
{
	ID_SEPARATOR,           // status line indicator
	ID_INDICATOR_X,
	ID_INDICATOR_Y,
	ID_INDICATOR_CAPS,
	ID_INDICATOR_NUM,
	ID_INDICATOR_SCRL,
};


// CMainFrame construction/destruction

CMainFrame::CMainFrame()
{
	// TODO: add member initialization code here
	chWnd=new CChildView();
	tools=new CToolDialog(this);
	m_sh = new CMathSheet("Math Functions", this);
}

CMainFrame::~CMainFrame()
{
	delete chWnd;
	delete tools;
	delete m_sh;
}


int CMainFrame::OnCreate(LPCREATESTRUCT lpCreateStruct)
{
	if (CFrameWnd::OnCreate(lpCreateStruct) == -1)
		return -1;
	
	if (!m_wndToolBar.CreateEx(this, TBSTYLE_FLAT, WS_CHILD | WS_VISIBLE | CBRS_TOP
		| CBRS_GRIPPER | CBRS_TOOLTIPS | CBRS_FLYBY | CBRS_SIZE_DYNAMIC	) ||
		!m_wndToolBar.LoadToolBar(IDR_MAINFRAME))
	{
		TRACE0("Failed to create toolbar\n");
		return -1;      // fail to create
	}


	if (!m_wndStatusBar.Create(this) ||
		!m_wndStatusBar.SetIndicators(indicators,
		  sizeof(indicators)/sizeof(UINT)))
	{
		TRACE0("Failed to create status bar\n");
		return -1;      // fail to create
	}

	chWnd->Create(NULL, NULL, WS_CHILD|WS_CLIPSIBLINGS|WS_CLIPCHILDREN, CRect(0, 0, 0,0),this, 100);
	chWnd->SetScale(AfxGetApp()->GetProfileIntA(s_Heading, s_Scale, 20000));
	chWnd->SetGrid(AfxGetApp()->GetProfileIntA(s_Heading, s_Grid, 1));
	curFile=AfxGetApp()->GetProfileString(s_Heading, s_File, "-1");
	if(curFile!="-1"){
		try{
			OpenFile();
		}
		catch (const exception& e){
			curFile="-1";
		}
		catch (const invalid_argument& e){
			curFile="-1";
		}
	}
	chWnd->ShowWindow(SW_SHOW);
	// TODO: Delete these three lines if you don't want the toolbar to be dockable
	m_wndToolBar.EnableDocking(CBRS_ALIGN_ANY);
	EnableDocking(CBRS_ALIGN_RIGHT);
	DockControlBar(&m_wndToolBar,AFX_IDW_DOCKBAR_RIGHT );
	tools->Create(IDD_TOOLS, this);
	tools->ReInit();
	tools->ShowWindow(SW_SHOWNORMAL);

	return 0;
}

BOOL CMainFrame::PreCreateWindow(CREATESTRUCT& cs)
{
	if( !CFrameWnd::PreCreateWindow(cs) )
		return FALSE;
	// TODO: Modify the Window class or styles here by modifying
	//  the CREATESTRUCT cs

	cs.style = WS_OVERLAPPED | WS_CAPTION | FWS_ADDTOTITLE
		 | WS_MINIMIZEBOX | WS_SYSMENU|WS_MAXIMIZEBOX|WS_CLIPCHILDREN;

	return TRUE;
}


// CMainFrame diagnostics

#ifdef _DEBUG
void CMainFrame::AssertValid() const
{
	CFrameWnd::AssertValid();
}

void CMainFrame::Dump(CDumpContext& dc) const
{
	CFrameWnd::Dump(dc);
}

#endif //_DEBUG


// CMainFrame message handlers



BOOL CMainFrame::LoadFrame(UINT nIDResource, DWORD dwDefaultStyle, CWnd* pParentWnd, CCreateContext* pContext) 
{
	// base class does the real work

	if (!CFrameWnd::LoadFrame(nIDResource, dwDefaultStyle, pParentWnd, pContext))
	{
		return FALSE;
	}

	CWinApp* pApp = AfxGetApp();
	if (pApp->m_pMainWnd == NULL)
		pApp->m_pMainWnd = this;

	return TRUE;
}

void CMainFrame::OnFileClose()
{
   DestroyWindow();
}

void CMainFrame::OnClose() 
{
	CFrameWnd::OnClose();
}

void CMainFrame::OnLButtonDown(UINT nFlags, CPoint point)
{
	// TODO: Add your message handler code here and/or call default
}

void CMainFrame::OnSize(UINT nType, int cx, int cy)
{
	CFrameWnd::OnSize(nType, cx, cy);
	CRect tmp;
	GetClientRect(&tmp);
	tools->MoveWindow(tmp.Height(), 0, tmp.Width()-tmp.Height()-30, tmp.Height()-20);
	tools->ShowWindow(SW_SHOW);
	// TODO: Add your message handler code here
}

void CMainFrame::SetCoord(CString chX, CString chY)
{
	m_wndStatusBar.SetPaneText(1, chX);
	m_wndStatusBar.SetPaneText(2, chY);
}

void CMainFrame::OnRButtonDown(UINT nFlags, CPoint point)
{
	// TODO: Add your message handler code here and/or call default
	CMenu pMenu;
	pMenu.LoadMenuA(IDR_MENUGRAPH);
	pMenu.GetSubMenu(0)->TrackPopupMenu(TPM_LEFTBUTTON, point.x, point.y, this);
	
	CFrameWnd::OnRButtonDown(nFlags, point);
}

void CMainFrame::OnGraphic()
{
	CGraphDialog graphDial(this);
	if (graphDial.DoModal()==IDOK){
		string tmp=const_cast<char*>(graphDial.m_Str.GetString());
		if (!baseGraph.addGraphic(tmp)){
			AfxMessageBox ("Invalid function! Try again.");
		}
		else{
			tools->ReInit();
		}
	}
	ChildReDraw();
}

void CMainFrame::OnPaint()
{
	CPaintDC dc(this); // device context for painting
	// TODO: Add your message handler code here
	// Do not call CFrameWnd::OnPaint() for painting messages
}

void CMainFrame::ChildReDraw(void)
{
	chWnd->Invalidate();
	chWnd->UpdateWindow();
}

void CMainFrame::OnPreferences()
{
	chWnd->OnPreferences();
}

void CMainFrame::OnToolsMath()
{
	m_sh->DoModal();
	ChildReDraw();
}

int CMainFrame::GetScale(){
	return (chWnd->GetScale());
}
int CMainFrame::GetNormScale(){
	return chWnd->GetNormScale();
}

void CMainFrame::OnDestroy()
{
	AfxGetApp()->WriteProfileInt(s_Heading, s_Scale, GetScale());
	AfxGetApp()->WriteProfileInt(s_Heading, s_Grid, chWnd->GetGrid());
	AfxGetApp()->WriteProfileString(s_Heading, s_File, curFile);
	CFrameWnd::OnDestroy();
}

void CMainFrame::OnFileOpen()
{
	CFileDialog fileDlg(TRUE, "grf", NULL, OFN_OVERWRITEPROMPT|
		OFN_PATHMUSTEXIST|OFN_HIDEREADONLY, "Graphic Files (*.grf)|*.grf\0", this);
	if (fileDlg.DoModal()==IDOK){
		curFile=fileDlg.GetPathName();
		try{
			OpenFile();
		}
		catch (const exception& e){
			AfxMessageBox(e.what());
		}
		catch (const invalid_argument& e){
			AfxMessageBox(e.what());
			curFile="-1";
		}
	}
	tools->ReInit();
	ChildReDraw();
}

void CMainFrame::OpenFile()
{
	ifstream fin(curFile);
	
	if (fin.fail())
		throw exception("Such file doesn't exist!");
	
	baseGraph.clear();
	
	int n;
	fin >> n;
	if (fin.fail())
		throw invalid_argument("File is corrupted!");
	
	
	string s;
	int tmp;

	for (int i=0; i<n; ++i){
		fin >> s >> tmp;
		if (fin.fail())
			throw invalid_argument("File is corrupted!");
		baseGraph.addGraphic(s);
		baseGraph.setDraw(tmp, baseGraph.size()-1);
	}
}

void CMainFrame::OnFileSave()
{
	CFileDialog fileDlg(FALSE, "grf", NULL, OFN_OVERWRITEPROMPT|
		OFN_PATHMUSTEXIST|OFN_HIDEREADONLY, "Graphic Files (*.grf)|*.grf\0", this);
	if (fileDlg.DoModal()==IDOK){
		curFile=fileDlg.GetPathName();
		try{
			SaveFile();
		}
		catch (const exception& e){
			AfxMessageBox(e.what());
		}
	}
	ChildReDraw();
}

void CMainFrame::SaveFile()
{
	ofstream fout(curFile);
	
	if (fout.fail())
		throw exception("Error opening file!");
	
	
	fout << baseGraph.size() << endl;	
	
	for (int i=0; i<baseGraph.size(); ++i){
		fout << baseGraph.getGraphic(i).GetString() << " " << baseGraph.getDraw(i) << endl;
	}
}
void CMainFrame::OnToolsTrace()
{
	CTracer* trcDlg = new CTracer(this);
	trcDlg->DoModal();
	delete trcDlg;
}
