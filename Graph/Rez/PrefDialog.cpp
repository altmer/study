// PrefDialog.cpp : implementation file
//

#include "stdafx.h"
#include "ChildView.h"
#include "MultTopLevel.h"
#include "PrefDialog.h"


// CPrefDialog dialog

IMPLEMENT_DYNAMIC(CPrefDialog, CDialog)

CPrefDialog::CPrefDialog(CWnd* pParent /*=NULL*/)
	: CDialog(CPrefDialog::IDD, pParent), b_Grid(FALSE), m_Scale(0)
{
	if (pParent!=NULL){
		b_Grid =(static_cast<CChildView*>(pParent))->GetGrid();
		m_Scale=(static_cast<CChildView*>(pParent))->GetScale()/(2*PIX);
	}
}

CPrefDialog::~CPrefDialog()
{
}

void CPrefDialog::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Check(pDX, IDC_CHECK1, b_Grid);
	DDX_Text(pDX, IDC_EDIT1, m_Scale);
}


BEGIN_MESSAGE_MAP(CPrefDialog, CDialog)
	ON_WM_CHANGEUISTATE()
	ON_WM_ASKCBFORMATNAME()
END_MESSAGE_MAP()


// CPrefDialog message handlers

void CPrefDialog::OnChangeUIState(UINT nAction, UINT nUIElement)
{
	// This feature requires Windows 2000 or greater.
	// The symbols _WIN32_WINNT and WINVER must be >= 0x0500.
	CDialog::OnChangeUIState(nAction, nUIElement);

	// TODO: Add your message handler code here
}

void CPrefDialog::OnAskCbFormatName(UINT nMaxCount, LPTSTR lpszString)
{
	// TODO: Add your message handler code here and/or call default

	CDialog::OnAskCbFormatName(nMaxCount, lpszString);
}
