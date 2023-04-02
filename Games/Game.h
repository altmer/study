#pragma once
#include "stdafx.h"

class Game
{
public:
	Game(void);
	virtual ~Game(void);
public:
	virtual void run()=0;
};
