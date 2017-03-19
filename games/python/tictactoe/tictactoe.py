#   tictactoe.py
#   author: Dean Marsinelli 3/19/2017
#
#   simple python tic tac toe game
#

BOARD_SIZE = 9

def InitializeBoard(board):
    board.clear()
    index = 0
    while (index < BOARD_SIZE):
        board.append(str(index))
        index += 1


def DrawBoard(board):
    index = 0
    for obj in board:
        if ((index % 3) != 2):
            print(' ' + board[index] + ' |', end='')
        else:
            print(' ' + board[index])
        index += 1;
    print('')


def CheckForWin(player, board):
    if (board[0] == player and board[1] == player and board[2] == player):
        return True
    elif (board[3] == player and board[4] == player and board[5] == player):
        return True
    elif (board[6] == player and board[7] == player and  board[8] == player):
        return True
    elif (board[0] == player and board[3] == player and board[6] == player):
        return True
    elif (board[1] == player and board[4] == player and board[7] == player):
        return True
    elif (board[2] == player and board[5] == player and board[8] == player):
        return True
    elif (board[0] == player and board[4] == player and board[8] == player):
        return True
    elif (board[2] == player and board[4] == player and board[6] == player):
        return True
    else: 
        return False


def PlayerMove(player, board):
    validMove = False

    while (not validMove):
        move = GetPlayerInput(player)
        if (not move == -1):
            validMove = True
        else:
            print('Invalid move. Try again.')
    board[move] = player


def GetPlayerInput(player):
    try:
        move = int(input('Player ' + player + ': Please select a valid move '))
        if (move < 0 or move > 8):
            return -1
        else:
            return move
    except ValueError:
        return -1


def ChangeTurn(player):
    if (player == 'X'):
        return 'O'
    else:
        return 'X'


def main():
    board = []
    currentPlayer = 'X'
    gameOver = False

    InitializeBoard(board)

    while (not gameOver):
        print('Current Player: ' + currentPlayer)
        DrawBoard(board)

        PlayerMove(currentPlayer, board)

        DrawBoard(board)

        gameOver = CheckForWin(currentPlayer, board)

        if (not gameOver):
            currentPlayer = ChangeTurn(currentPlayer)
        

main()
