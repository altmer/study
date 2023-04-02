#pragma once


// CMySlider

class CMySlider : public CSliderCtrl
{
	DECLARE_DYNAMIC(CMySlider)

public:
	CMySlider(CWnd* par = NULL);
	virtual ~CMySlider();
	CWnd* p;

protected:
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnMouseMove(UINT nFlags, CPoint point);
};


