#pragma once
#include "Function.h"

class Math
{
protected:
	Math(void);
	virtual ~Math(void) = 0;
	static double sol_half (double a, double b, CFunction& f);
public:
	static vector <double> findRoot(double A, double B, CFunction& f)throw (invalid_argument);
	static double integrate (double a, double b, CFunction& f) throw (invalid_argument);
};
