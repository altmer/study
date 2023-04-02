// IntegrPage.cpp : implementation file
//

#include "stdafx.h"
#include "MultTopLevel.h"
#include "Roots.h"
#include "MainFrm.h"
#include "Math.h"
#include <vector>
using namespace std;

// IntegrPage dialog

IMPLEMENT_DYNAMIC(IntegrPage, CPropertyPage)

IntegrPage::IntegrPage()
	: CPropertyPage(IntegrPage::IDD)
	, m_From(0)
	, m_To(0)
	, m_Result(0)
{

}

IntegrPage::~IntegrPage()
{
}

void IntegrPage::DoDataExchange(CDataExchange* pDX)
{
	CPropertyPage::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_COMBO1, m_Com);
	DDX_Text(pDX, IDC_EDIT2, m_From);
	DDX_Text(pDX, IDC_EDIT3, m_To);
	DDX_Text(pDX, IDC_EDIT1, m_Result);
	DDX_Control(pDX, IDC_EDIT2, m_EFrom);
	DDX_Control(pDX, IDC_EDIT3, m_ETo);
	DDX_Control(pDX, IDC_EDIT1, m_Ans);
}


BEGIN_MESSAGE_MAP(IntegrPage, CPropertyPage)
	ON_BN_CLICKED(IDC_BUTTON1, &IntegrPage::OnBnClickedButton1)
END_MESSAGE_MAP()


// IntegrPage message handlers

void IntegrPage::OnOK()
{
	// TODO: Add your specialized code here and/or call the base class

	//CPropertyPage::OnOK();
}

void IntegrPage::OnCancel()
{
	// TODO: Add your specialized code here and/or call the base class

	//CPropertyPage::OnCancel();
}

void IntegrPage::OnBnClickedButton1()
{
	double res=-1;
	try{
		int nIndex=m_Com.GetCurSel();
		if (nIndex!=-1){
			CMainFrame* ptr=static_cast<CMainFrame*>(par);
			CString str;
			m_EFrom.GetWindowText(str);
			double a, b;
			sscanf (str, "%lf", &a);
			m_ETo.GetWindowText(str);
			sscanf (str, "%lf", &b);
			res = Math::integrate(a, b, ptr->baseGraph.getGraphic(nIndex));
		}
	}
	catch (const invalid_argument& e){
		AfxMessageBox(e.what());
		return;
	}
	char str[100];
	sprintf (str, "%.4lf", res);
	m_Ans.SetWindowTextA(str);
}

BOOL IntegrPage::OnInitDialog()
{
	CPropertyPage::OnInitDialog();
	m_Com.ResetContent();
	CMainFrame* ptr=static_cast<CMainFrame*>(par);
	for (int i=0; i<ptr->baseGraph.size();++i){
		m_Com.AddString(const_cast<char*>(ptr->baseGraph.getGraphic(i).GetString()));
	}
	int a=-ptr->GetNormScale();
	int b=-a;
	char str[100];
	sprintf (str, "%d", a);
	m_EFrom.SetWindowTextA(str);
	sprintf (str, "%d", b);
	m_ETo.SetWindowTextA(str);
	return TRUE;  // return TRUE unless you set the focus to a control
	// EXCEPTION: OCX Property Pages should return FALSE
}
