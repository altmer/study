#pragma once


// CGraphDialog dialog

class CGraphDialog : public CDialog
{
	DECLARE_DYNAMIC(CGraphDialog)

public:
	CGraphDialog(CWnd* pParent = NULL);   // standard constructor
	virtual ~CGraphDialog();

// Dialog Data
	enum { IDD = IDD_GRAPHIC };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	CString m_Str;
public:
	afx_msg void OnPaint();
};
