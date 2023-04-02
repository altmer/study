#pragma once
#include "afxwin.h"
#include <exception>
#include <vector>
#include <cmath>

// IntegrPage dialog

class IntegrPage : public CPropertyPage
{
	DECLARE_DYNAMIC(IntegrPage)

public:
	IntegrPage();
	virtual ~IntegrPage();

// Dialog Data
	enum { IDD = IDD_INTEGR };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	CWnd* par;
	virtual void OnOK();
	virtual void OnCancel();
	CComboBox m_Com;
	int m_From;
	int m_To;
	double m_Result;
	afx_msg void OnBnClickedButton1();
	virtual BOOL OnInitDialog();
	CEdit m_EFrom;
	CEdit m_ETo;
	CEdit m_Ans;
};
