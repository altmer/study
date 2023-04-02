// ToolDialog.cpp : implementation file
//

#include "stdafx.h"
#include "MultTopLevel.h"
#include "ToolDialog.h"
#include "MainFrm.h"


// CToolDialog dialog

IMPLEMENT_DYNAMIC(CToolDialog, CDialog)

CToolDialog::CToolDialog(CWnd* pParent /*=NULL*/)
	: CDialog(CToolDialog::IDD, pParent)
	, b_Draw(FALSE)
{
}

CToolDialog::~CToolDialog()
{
}

void CToolDialog::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Check(pDX, IDC_CHECK2, b_Draw);
	DDX_Control(pDX, IDC_CHECK2, Check);
	DDX_Control(pDX, IDC_LIST1, m_List);
	DDX_Control(pDX, IDC_COLOR_BUT, ColorBut);
	DDX_Control(pDX, IDC_BUTTON2, DeleteBut);
	DDX_Control(pDX, IDC_BUTTON3, AddBut);
}


BEGIN_MESSAGE_MAP(CToolDialog, CDialog)
	ON_WM_CREATE()
	ON_WM_KEYDOWN()
	ON_WM_KEYUP()
	ON_WM_CLOSE()
	ON_WM_RBUTTONDOWN()
	ON_BN_CLICKED(IDC_CHECK2, &CToolDialog::OnBnClickedCheck1)
	ON_BN_CLICKED(IDC_BUTTON1, &CToolDialog::OnBnClickedButton1)
	ON_BN_CLICKED(IDC_BUTTON2, &CToolDialog::OnBnClickedButton2)
	ON_BN_CLICKED(IDC_BUTTON3, &CToolDialog::OnBnClickedButton3)
	ON_LBN_SELCHANGE(IDC_LIST1, &CToolDialog::OnLbnSelchangeList1)
END_MESSAGE_MAP()


// CToolDialog message handlers

int CToolDialog::OnCreate(LPCREATESTRUCT lpCreateStruct)
{
	if (CDialog::OnCreate(lpCreateStruct) == -1)
		return -1;

	return 0;
}


void CToolDialog::ReInit(void)
{
	m_List.ResetContent();
	CMainFrame* ptr=static_cast<CMainFrame*>(GetParent());
	for (int i=0; i<ptr->baseGraph.size();++i){
		m_List.AddString(const_cast<char*>(ptr->baseGraph.getGraphic(i).GetString()));
	}
}

void CToolDialog::OnKeyDown(UINT nChar, UINT nRepCnt, UINT nFlags)
{
}

void CToolDialog::OnKeyUp(UINT nChar, UINT nRepCnt, UINT nFlags)
{
}

void CToolDialog::OnClose()
{
	CDialog::OnClose();
}

void CToolDialog::OnRButtonDown(UINT nFlags, CPoint point)
{
	CRect tmp;
	CMainFrame* ptr=static_cast<CMainFrame*>(GetParent());
	ptr->GetClientRect(&tmp);
	point.x+=tmp.Height()-20;
	ptr->OnRButtonDown(nFlags, point);
}

void CToolDialog::OnBnClickedCheck1()
{
	int nIndex=m_List.GetCurSel();
	if (nIndex!=CB_ERR){
		CMainFrame* ptr=static_cast<CMainFrame*>(GetParent());
		ptr->baseGraph.setDraw(Check.GetCheck(),nIndex);
		ptr->ChildReDraw();
	}
}

void CToolDialog::OnBnClickedButton1()
{
	int nIndex=m_List.GetCurSel();
	if (nIndex!=CB_ERR){
		CMainFrame* ptr=static_cast<CMainFrame*>(GetParent());
		CColorDialog colorDia;
		if (colorDia.DoModal()==IDOK){
			ptr->baseGraph.setColor(colorDia.GetColor(),nIndex);
		}
		ptr->ChildReDraw();
	}
}

void CToolDialog::OnBnClickedButton2()
{
	int nIndex=m_List.GetCurSel();
	if (nIndex!=CB_ERR){
		CMainFrame* ptr=static_cast<CMainFrame*>(GetParent());
		ptr->baseGraph.deleteGraphic(nIndex);
		ptr->ChildReDraw();
		ReInit();
	}
}

void CToolDialog::OnBnClickedButton3()
{
	CMainFrame* ptr=static_cast<CMainFrame*>(GetParent());
	ptr->OnGraphic();
}

void CToolDialog::OnLbnSelchangeList1()
{
	int nIndex=m_List.GetCurSel();
	if (nIndex!=CB_ERR){
		CMainFrame* ptr=static_cast<CMainFrame*>(GetParent());
		Check.SetCheck(ptr->baseGraph.getDraw(nIndex));
	}
}

void CToolDialog::OnOK()
{
	// TODO: Add your specialized code here and/or call the base class

}

void CToolDialog::OnCancel()
{
	// TODO: Add your specialized code here and/or call the base class
}
