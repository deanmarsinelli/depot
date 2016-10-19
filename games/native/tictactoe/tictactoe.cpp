#include <iostream>
#include <cstdlib>

const int BOARD_SIZE = 9;

using namespace std;

// initialize all the squares
void InitializeBoard(char* board)
{
    for (int i = 0; i < BOARD_SIZE; i++)
    {
        board[i] = i + '0';
    }
}

// draw current state of the board
void DrawBoard(const char* board)
{
    for (int i = 0; i < BOARD_SIZE; i++)
    {
        if ((i % 3) != 2)
        {
            cout << " " << board[i] << " |";
        }
        else
        {
            // this handles the right hand column
            cout << " " << board[i] << endl;
        }
    }
    cout << endl << endl;
}

void PlayerMove(const char player, char* board)
{
    bool validMove = false;
    int move;

    do
    {
        cout << "Player " << player << ": Please select a valid move ";
        cin >> move;

        if (move < 0 || move > 8)
        {
            cout << "Invalid Move" << endl;
            continue;
        }

        // convert move to a char since our board is stored as chars
        char moveAsChar = move + '0';
        if (moveAsChar == board[move])
        {
            // if the values match, it is a valid move
            board[move] = player;
            validMove = true;
        }
        else
        {
            cout << "Invalid Move" << endl;
        }

    } while (validMove == false);
}

bool CheckForWin(const char player, const char* board)
{
    // player holds 'X' or 'O'
    // these are all the combinations a win can happen
    if (board[0] == player && board[1] == player && board[2] == player)
        return true;
    else if (board[3] == player && board[4] == player && board[5] == player)
        return true;
    else if (board[6] == player && board[7] == player && board[8] == player)
        return true;
    else if (board[0] == player && board[3] == player && board[6] == player)
        return true;
    else if (board[1] == player && board[4] == player && board[7] == player)
        return true;
    else if (board[2] == player && board[5] == player && board[8] == player)
        return true;
    else if (board[0] == player && board[4] == player && board[8] == player)
        return true;
    else if (board[2] == player && board[4] == player && board[6] == player)
        return true;
    else return false;
}

char ChangeTurn(const char player)
{
    // if player is X, return O
    if (player == 'X')
    {
        return 'O';
    }
    // otherwise if player is O return X
    else
    {
        return 'X';
    }
}

int main()
{
    char board[BOARD_SIZE];

    // whos turn is it?
    char currentPlayer = 'X';
    bool gameOver = false;

    // init the game
    InitializeBoard(board);

    while (!gameOver)
    {
        cout << "Current Player: " << currentPlayer << endl;
        // 1) draw the board
        DrawBoard(board);

        // 2) players turn
        PlayerMove(currentPlayer, board);

        // 3) draw board again
        DrawBoard(board);

        // 4) check for win condition
        gameOver = CheckForWin(currentPlayer, board);

        if (!gameOver)
        {
            // 5) switch turn
            currentPlayer = ChangeTurn(currentPlayer);
        }
    }

    cout << "Player " << currentPlayer << " won the game!" << endl;

    return 0;
}
