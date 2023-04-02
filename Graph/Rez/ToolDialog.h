#pragma once
#include "afxwin.h"


// CToolDialog dialog

class CToolDialog : public CDialog
{
	DECLARE_DYNAMIC(CToolDialog)

public:
	CToolDialog(CWnd* pParent = NULL);   // standard constructor
	virtual ~CToolDialog();

// Dialog Data
	enum { IDD = IDD_TOOLS };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	afx_msg int OnCreate(LPCREATESTRUCT lpCreateStruct);
public:
	CComboBox m_Combo;
public:
	void ReInit(void);
public:
	afx_msg void OnKeyDown(UINT nChar, UINT nRepCnt, UINT nFlags);
public:
	afx_msg void OnKeyUp(UINT nChar, UINT nRepCnt, UINT nFlags);
public:
	afx_msg void OnClose();
public:
	afx_msg void OnRButtonDown(UINT nFlags, CPoint point);
public:
	afx_msg void OnCbnSelchangeCombo1();
public:
	afx_msg void OnBnClickedCheck1();
public:
	BOOL b_Draw;
public:
	CButton Check;
public:
	afx_msg void OnBnClickedButton1();
public:
	afx_msg void OnBnClickedButton2();
public:
	afx_msg void OnBnClickedButton3();
};
