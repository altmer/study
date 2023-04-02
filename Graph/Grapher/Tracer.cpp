// Tracer.cpp : implementation file
//

#include "stdafx.h"
#include "afxcmn.h"
#include "commctrl.h"
#include "MultTopLevel.h"
#include "Tracer.h"
#include "MainFrm.h"
#include "ChildView.h"


// CTracer dialog

IMPLEMENT_DYNAMIC(CTracer, CDialog)

CTracer::CTracer(CWnd* pParent /*=NULL*/)
	: CDialog(CTracer::IDD, pParent)
	, m_X(0)
	, m_Y(0), m_Trace(this)
{
	
}

CTracer::~CTracer()
{
}

void CTracer::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_SLIDER1, m_Trace);
	DDX_Control(pDX, IDC_COMBO1, m_Com);
	DDX_Text(pDX, IDC_EDIT1, m_X);
	DDX_Text(pDX, IDC_EDIT2, m_Y);
}


BEGIN_MESSAGE_MAP(CTracer, CDialog)
	ON_CBN_SELCHANGE(IDC_COMBO1, &CTracer::OnCbnSelchangeCombo1)
	ON_WM_LBUTTONUP()
	ON_WM_MOUSEMOVE()
END_MESSAGE_MAP()


// CTracer message handlers

BOOL CTracer::OnInitDialog()
{
	CDialog::OnInitDialog();

	CMainFrame* ptr=(CMainFrame*)GetParent();
	m_Trace.SetRange(-ptr->GetScale()/2, ptr->GetScale()/2, TRUE);
	m_Trace.SetPos(m_Trace.GetRangeMin());
	m_X = double(m_Trace.GetRangeMin())/double(PIX);
	m_Y = -1;
	m_Com.ResetContent();
	for (int i=0; i<ptr->baseGraph.size(); ++i){
		m_Com.AddString(const_cast<char*>(ptr->baseGraph.getGraphic(i).GetString()));
	}
	UpdateData(FALSE);

	return TRUE;  // return TRUE unless you set the focus to a control
	// EXCEPTION: OCX Property Pages should return FALSE
}

void CTracer::OnCbnSelchangeCombo1()
{
	int nIndex=m_Com.GetCurSel();
	if (nIndex!=-1){
		CMainFrame* ptr=(CMainFrame*)GetParent();
		m_Y = ptr->baseGraph.getGraphic(nIndex)(m_X);
		UpdateData(FALSE);
	}
}

void CTracer::OnLButtonUp(UINT nFlags, CPoint point)
{
	m_X=double(m_Trace.GetPos())/double(PIX);
	int nIndex = m_Com.GetCurSel();
	if (nIndex!=-1){
		CMainFrame* ptr=(CMainFrame*)GetParent();
		m_Y = ptr->baseGraph.getGraphic(nIndex)(m_X);
	}
	UpdateData(FALSE);
	CDialog::OnLButtonUp(nFlags, point);
}

void CTracer::OnMouseMove(UINT nFlags, CPoint point)
{
	m_X=double(m_Trace.GetPos())/double(PIX);
	int nIndex = m_Com.GetCurSel();
	if (nIndex!=-1){
		CMainFrame* ptr=(CMainFrame*)GetParent();
		m_Y = ptr->baseGraph.getGraphic(nIndex)(m_X);
	}
	UpdateData(FALSE);
	CDialog::OnMouseMove(nFlags, point);
}
