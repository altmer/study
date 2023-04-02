// Roots.cpp : implementation file
//

#include "stdafx.h"
#include "MultTopLevel.h"
#include "Roots.h"
#include "MainFrm.h"
#include "Math.h"
#include <vector>
using namespace std;

// Roots dialog

IMPLEMENT_DYNAMIC(Roots, CPropertyPage)

Roots::Roots()
	: CPropertyPage(Roots::IDD)
	, m_From(0)
	, m_To(0)
{

}

Roots::~Roots()
{
}

void Roots::DoDataExchange(CDataExchange* pDX)
{
	CPropertyPage::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_COMBO1, m_Fun);
	DDX_Text(pDX, IDC_EDIT2, m_From);
	DDX_Text(pDX, IDC_EDIT3, m_To);
	DDX_Control(pDX, IDC_EDIT2, m_EFrom);
	DDX_Control(pDX, IDC_EDIT3, m_ETo);
	DDX_Control(pDX, IDC_LIST1, m_Ans);
}


BEGIN_MESSAGE_MAP(Roots, CPropertyPage)
	ON_BN_CLICKED(IDC_Calc, &Roots::OnBnClickedCalc)
END_MESSAGE_MAP()


// Roots message handlers

void Roots::OnBnClickedCalc()
{
	vector <double> root;
	try{
		int nIndex=m_Fun.GetCurSel();
		if (nIndex!=-1){
			CMainFrame* ptr=static_cast<CMainFrame*>(par);
			CString str;
			m_EFrom.GetWindowText(str);
			sscanf (str, "%lf", &m_From);
			m_ETo.GetWindowText(str);
			sscanf (str, "%lf", &m_To);
			root = Math::findRoot(m_From, m_To, ptr->baseGraph.getGraphic(nIndex));
		}
	}
	catch (const invalid_argument& e){
		AfxMessageBox(e.what());
		return;
	}
	m_Ans.ResetContent();
	for (int i=0; i<root.size(); ++i){
		char str[100];
		sprintf (str, "%.3lf", root[i]);
		m_Ans.AddString(str);
	}
}

void Roots::OnCancel()
{
	// TODO: Add your specialized code here and/or call the base class

	//CPropertyPage::OnCancel();
}

void Roots::OnOK()
{
	// TODO: Add your specialized code here and/or call the base class

	//CPropertyPage::OnOK();
}

BOOL Roots::OnInitDialog()
{
	CPropertyPage::OnInitDialog();
	m_Fun.ResetContent();
	CMainFrame* ptr=static_cast<CMainFrame*>(par);
	for (int i=0; i<ptr->baseGraph.size();++i){
		m_Fun.AddString(const_cast<char*>(ptr->baseGraph.getGraphic(i).GetString()));
	}
	int a=-ptr->GetNormScale();
	int b=ptr->GetNormScale();
	char str[100];
	sprintf (str, "%d", a);
	m_EFrom.SetWindowTextA(str);
	sprintf (str, "%d", b);
	m_ETo.SetWindowTextA(str);
	
	return TRUE;  // return TRUE unless you set the focus to a control
	// EXCEPTION: OCX Property Pages should return FALSE
}
