#pragma once
#include "game.h"
using namespace std;

class CxZ :
	public Game
{
public:
	enum {F_WIN, S_WIN, UNDETER, DRAW};

	class Position{
	protected:
		vector <vector <char> > grid;
	public:
		Position(int n = 3);
		~Position();
		char get(int wh) ;
		void set(char ch, int wh);
		void clear(int wh);
		int size() ;
		int res_game() ;
		long long hash() ;
		void unhash(long long h);
		void draw() ;
		bool isFree(int wh) ;
	};

	class Player{
	public:
		Player();
		~Player();
		virtual void init(int n)=0;
		virtual void make_turn(Position& pos, int pl)=0;
	};

	class PlayerHARD : public Player{
	protected:
		map <long long, int> pos_num;
		vector <vector <int> > tree;
		vector <long long> codes;
		vector <int> win;
		vector <int> turn;
		int dfs1(int);
		int dfs2(int);
	public:
		PlayerHARD();
		~PlayerHARD();
		void make_turn(Position& pos, int pl);
		void init(int n=3);
	private: // some temporary data
		vector <int> par;
		vector <int> res;
	};

	class PlayerEASY : public Player{
	protected:
	public:
		PlayerEASY();
		~PlayerEASY();
		void make_turn(Position& pos, int pl);
		void init(int n=3);
	private: 
		int count ( vector<char>const& bef,  vector <char>const& aft, char ch) const;
	};

public:
	CxZ(int n = 3, bool hard=true, bool isPvP = false);
	~CxZ(void);
protected:
	Player* AI;
	Position cPos;
	char sym[2];
	int cMove, rasm;
	bool isPlayer;
public: 
	static int tow;
public:
	void run();
protected:
	void draw();
	void player_turn();
};
