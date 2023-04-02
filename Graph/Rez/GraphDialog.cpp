// GraphDialog.cpp : implementation file
//

#include "stdafx.h"
#include "MultTopLevel.h"
#include "GraphDialog.h"


// CGraphDialog dialog

IMPLEMENT_DYNAMIC(CGraphDialog, CDialog)

CGraphDialog::CGraphDialog(CWnd* pParent /*=NULL*/)
	: CDialog(CGraphDialog::IDD, pParent)
	, m_Str(_T(""))
{

}

CGraphDialog::~CGraphDialog()
{
}

void CGraphDialog::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Text(pDX, IDC_EDIT1, m_Str);
}


BEGIN_MESSAGE_MAP(CGraphDialog, CDialog)
	ON_WM_PAINT()
END_MESSAGE_MAP()


// CGraphDialog message handlers

void CGraphDialog::OnPaint()
{
	CPaintDC dc(this); // device context for painting
	// TODO: Add your message handler code here
	// Do not call CDialog::OnPaint() for painting messages
}
