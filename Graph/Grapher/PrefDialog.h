#pragma once
#include "afxcmn.h"
#include "afxwin.h"


// CPrefDialog dialog

class CPrefDialog : public CDialog
{
	DECLARE_DYNAMIC(CPrefDialog)

public:
	CPrefDialog(CWnd* pParent = NULL);   // standard constructor
	virtual ~CPrefDialog();

// Dialog Data
	enum { IDD = IDD_PREFDIALOG };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	BOOL b_Grid;
public:
	int m_Scale;
public:
	CSpinButtonCtrl m_Spin;
public:
	CEdit m_Edit;
public:
	virtual BOOL OnInitDialog();
};
