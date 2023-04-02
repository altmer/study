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
	DDX_Control(pDX, IDC_SPIN1, m_Spin);
	DDX_Control(pDX, IDC_EDIT1, m_Edit);
}


BEGIN_MESSAGE_MAP(CPrefDialog, CDialog)
END_MESSAGE_MAP()


// CPrefDialog message handlers


BOOL CPrefDialog::OnInitDialog()
{
	CDialog::OnInitDialog();

	m_Spin.SetBuddy(&m_Edit);
	m_Spin.SetRange(5, 25);
	m_Spin.SetPos(m_Scale);
	m_Edit.SetFocus();

	return TRUE;  // return TRUE unless you set the focus to a control
	// EXCEPTION: OCX Property Pages should return FALSE
}
