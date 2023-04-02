#pragma once


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
	afx_msg void OnChangeUIState(UINT nAction, UINT nUIElement);
public:
	afx_msg void OnAskCbFormatName(UINT nMaxCount, LPTSTR lpszString);
};
