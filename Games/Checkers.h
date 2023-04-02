#pragma once
#include "game.h"
using namespace std;

class Checkers :
	public Game
{
public:
	Checkers(bool pvp);
	~Checkers(void);

	enum {BLACK = 0, WHITE= 1};
	enum {F_WIN = 0, S_WIN = 1, UNDETER = 2};
	enum {BEAT = 0, NOT_BEATEN = 1, BEATEN = 2, CANNOT = 3};

	class Position;

	class Piece{
	protected:
		int x, y;
		int bestMove;
	public:
		int type;
		bool king;
		int can_move (Checkers::Position& pos);
		bool beats (int cx, int cy,Checkers::Position& pos);
		void printXY();
		pair <int, int> getCoord();
		void move (int tox, int toy);
		int getBestMove();
		pair <pair <int, int>, pair <int, int> > getMove(Checkers::Position& pos);
		bool operator == (const Piece& T) const;
		Piece(int _x, int _y, int _t);
		~Piece();
		static int dx[4], dy[4];
		friend bool cmp (Checkers::Piece*const& T1,Checkers::Piece*const& T2);
	};

	class Position{
		friend class Piece;
	public:
		Position();
		~Position();
		void draw();
		int res_game();
		vector <Piece*> kings (int pl);
		vector <Piece*> toBeat (int pl);
		static bool on_board(int cx, int cy);
		bool move( int frx, int fry, int tox, int toy, bool capt, int pl);
		bool can_beat(int x,int y, int pl);
		void del( Piece * ptr);
		vector <Piece*> getMyPieces(int pl);
		Piece* toMove;
		Piece* getPtr(int x, int y);
	protected:
		list <Piece> pPieces[2];
		vector <vector < Piece* > > grid;
	};

	class Player{
	public:
		Player();
		~Player();
	public:
		void make_turn (Checkers::Position& pos, int pl);
	};

protected:
	Position pos;
	Player* AI;
	bool isPlayer;
	int cMove;
public:
	void run();
protected:
	void draw();
	bool player_turn();
};
