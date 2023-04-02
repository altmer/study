#pragma once
#include "afxwin.h"
#include <exception>
#include <cmath>
#include <vector>
#include <string>


// Roots dialog

class Roots : public CPropertyPage
{
	DECLARE_DYNAMIC(Roots)

public:
	Roots();
	virtual ~Roots();

// Dialog Data
	enum { IDD = IDD_ROOTS };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	CWnd* par;
	CComboBox m_Fun;
	double m_From;
	double m_To;
	afx_msg void OnBnClickedCalc();
	virtual void OnCancel();
	virtual void OnOK();
	virtual BOOL OnInitDialog();
	CEdit m_EFrom;
	CEdit m_ETo;
	CListBox m_Ans;
};
