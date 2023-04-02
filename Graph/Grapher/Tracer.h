#pragma once
#include "afxcmn.h"
#include "afxwin.h"
#include "MySlider.h"

// CTracer dialog

class CTracer : public CDialog
{
	DECLARE_DYNAMIC(CTracer)

public:
	CTracer(CWnd* pParent = NULL);   // standard constructor
	virtual ~CTracer();

// Dialog Data
	enum { IDD = IDD_TRACE };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	CMySlider m_Trace;
	CComboBox m_Com;
	double m_X;
	double m_Y;
	virtual BOOL OnInitDialog();
	afx_msg void OnCbnSelchangeCombo1();
	afx_msg void OnTRBNThumbPosChangingSlider1(NMHDR *pNMHDR, LRESULT *pResult);
	afx_msg void OnLButtonUp(UINT nFlags, CPoint point);
	afx_msg void OnMouseMove(UINT nFlags, CPoint point);
};
