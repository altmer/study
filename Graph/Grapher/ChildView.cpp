// ChildView.cpp : implementation of the CChildView class
//

#include "stdafx.h"
#include "MultTopLevel.h"
#include "ChildView.h"
#include "MainFrm.h"
#include <cmath>
#include <iostream>
#include <sstream>
#include <string>
#include "PrefDialog.h"
using namespace std;

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CChildView

CChildView::CChildView() : m_Scale(20*PIX), b_Grid(true)
{
}

CChildView::~CChildView()
{
}


BEGIN_MESSAGE_MAP(CChildView, CWnd)
	ON_WM_PAINT()
	ON_WM_CREATE()
	ON_WM_MOUSEMOVE()
	ON_WM_RBUTTONDOWN()
	ON_WM_SIZE()
	ON_WM_WINDOWPOSCHANGED()
	ON_COMMAND(ID_PREFERENCES, &CChildView::OnPreferences)
	ON_COMMAND(ID_RENEW, &CChildView::OnRenew)
	ON_WM_MOVE()
END_MESSAGE_MAP()



// CChildView message handlers

BOOL CChildView::PreCreateWindow(CREATESTRUCT& cs) 
{
	if (!CWnd::PreCreateWindow(cs))
		return FALSE;

	cs.dwExStyle |= WS_EX_CLIENTEDGE;
	cs.style &= ~WS_BORDER;
	cs.lpszClass = AfxRegisterWndClass(CS_HREDRAW|CS_VREDRAW|CS_DBLCLKS, 
		::LoadCursor(NULL, IDC_ARROW), reinterpret_cast<HBRUSH>(COLOR_WINDOW+1), NULL);

	return TRUE;
}

void CChildView::OnPaint() 
{
	CPaintDC dc(this); // device context for painting
	CRect tmp;
	GetParent()->GetClientRect(&tmp);
	MoveWindow(0, 0, tmp.Height()-20, tmp.Height()-20);
		
	// TODO: Add your message handler code here
	// background

	dc.FloodFill(0, 0, RGB(255, 255, 255));
	// Coordinates

	GetClientRect(&tmp);
	dc.SetMapMode(MM_ISOTROPIC);
	dc.SetWindowExt(m_Scale, m_Scale);
	dc.SetViewportExt(tmp.Width(), -tmp.Height());
	dc.SetViewportOrg(tmp.Width()/2, tmp.Height()/2);
	char* str = new char[10];
	for (int x=-m_Scale/2; x<=m_Scale/2; x+=PIX){
		sprintf (str, "%d", x/PIX);
		dc.TextOutA(x, -5, str);
	}
	for (int y=-m_Scale/2; y<=m_Scale/2; y+=PIX){
		sprintf (str, "%d", y/PIX);
		dc.TextOutA(5, y, str);
	}
	delete [] str;
	// grid
	if (b_Grid){
		dc.SelectObject(m_gridPen);
		for (int x=-m_Scale/2; x<=m_Scale/2; x+=PIX){
			dc.MoveTo(x, m_Scale);
			dc.LineTo(x, -m_Scale);
		}
		for (int y=-m_Scale/2; y<=m_Scale/2; y+=PIX){
			dc.MoveTo(m_Scale, y);
			dc.LineTo(-m_Scale, y);
		}
	}

	// Axis
	dc.SelectObject(m_axisPen);
	dc.MoveTo(0, -m_Scale);
	dc.LineTo(0, m_Scale);
	dc.MoveTo(-m_Scale, 0);
	dc.LineTo(m_Scale, 0);

	// Graphics
	CGrapher* pGr=&(static_cast<CMainFrame*>(GetParentFrame())->baseGraph);
	for (int i=0; i<pGr->size(); ++i){
		if (pGr->getDraw(i))
			DrawGraphic(&dc, pGr->getGraphic(i), pGr->getColor(i));
	}

	// Do not call CWnd::OnPaint() for painting messages
}


int CChildView::OnCreate(LPCREATESTRUCT lpCreateStruct)
{
	if (CWnd::OnCreate(lpCreateStruct) == -1)
		return -1;
	CRect tmp;
	GetParent()->GetClientRect(&tmp);
	MoveWindow(0, 0, tmp.Height()-20, tmp.Height()-20);
	::SetClassLong(GetSafeHwnd() , GCL_HCURSOR, (LONG)AfxGetApp()->LoadStandardCursor(IDC_CROSS));
	m_axisPen.CreatePen(PS_SOLID, 10, RGB(0, 0, 0));
	m_gridPen.CreatePen(PS_DOT, 0, RGB(0, 0, 0));

	// TODO:  Add your specialized creation code here

	return 0;
}

void CChildView::OnMouseMove(UINT nFlags, CPoint point)
{
	CDC* dc=GetDC();
	CRect tmp;
	GetClientRect(&tmp);
	dc->SetMapMode(MM_ISOTROPIC);
	dc->SetWindowExt(m_Scale, m_Scale);
	dc->SetViewportExt(tmp.Width(), -tmp.Height());
	dc->SetViewportOrg(tmp.Width()/2, tmp.Height()/2);
	dc->DPtoLP(&point);
	double x=static_cast<double>(point.x)/static_cast<double>(PIX);
	double y=static_cast<double>(point.y)/static_cast<double>(PIX);
	CString chX, chY;
	string s;
	ostringstream iss;
	iss<<x;
	s=iss.str();
	iss.str("");
	for (int i=0;i<s.size(); ++i){
		chX.AppendChar(s[i]);
	}
	iss<<y;
	s=iss.str();
	iss.str("");
	for (int i=0;i<s.size(); ++i){
		chY.AppendChar(s[i]);
	}
	static_cast<CMainFrame*>(GetParentFrame())->SetCoord(chX, chY);
}

void CChildView::SetScale(int newScale)
{
	m_Scale=newScale;
}

void CChildView::SetGrid(bool newGrid)
{
	b_Grid=newGrid;
}

void CChildView::OnRButtonDown(UINT nFlags, CPoint point)
{
	CMenu popMenu;
	popMenu.LoadMenu(IDR_MENU_DRAW);
	popMenu.GetSubMenu(0)->TrackPopupMenu(TPM_LEFTBUTTON|TPM_LEFTALIGN, point.x, point.y, this);

	CWnd::OnRButtonDown(nFlags, point);
}

void CChildView::OnSize(UINT nType, int cx, int cy)
{
	CWnd::OnSize(nType, cx, cy);
	// TODO: Add your message handler code here
}

void CChildView::OnWindowPosChanged(WINDOWPOS* lpwndpos)
{
	CWnd::OnWindowPosChanged(lpwndpos);
	Invalidate();
	UpdateWindow();

	// TODO: Add your message handler code here
}

void CChildView::DrawGraphic(CPaintDC* dc, CFunction& fun, COLORREF col)
{
	dc->MoveTo(-m_Scale/2-10,0);
	CPen tmp(PS_SOLID,0,col);
	dc->SelectObject(tmp);
	int prev=0;
	for (int x=-m_Scale/2; x<=m_Scale/2; ++x){
		double y=fun(((double)x/double(PIX)));
		int _y=static_cast<int>(y*PIX);
		dc->SetPixel(x, _y, col);
		if (abs(prev)<1e+6)
			dc->LineTo(x,_y);
		else
			dc->MoveTo(x,_y);
		prev=_y;
	}
}

void CChildView::OnPreferences()
{
	CPrefDialog prefDialog(this);
	if(prefDialog.DoModal()==IDOK){
		int tmp=prefDialog.m_Scale*2*PIX;
		if (tmp && tmp!=m_Scale){
			if (tmp<10000)
				AfxMessageBox("Too small scale!");
			else if (tmp>50000)
				AfxMessageBox ("Too big scale!");
			else
				SetScale(tmp);
		}
		SetGrid(prefDialog.b_Grid);
	}
	Invalidate();
	UpdateWindow();
}

void CChildView::OnRenew()
{
	// TODO: Add your command handler code here
	Invalidate();
	UpdateWindow();
}

int CChildView::GetScale()
{
	return m_Scale;
}

int CChildView::GetNormScale()
{
	return m_Scale/(2*PIX);
}

bool CChildView::GetGrid()
{
	return b_Grid;
}

void CChildView::OnMove(int x, int y)
{
	CWnd::OnMove(x, y);

	// TODO: Add your message handler code here
}
