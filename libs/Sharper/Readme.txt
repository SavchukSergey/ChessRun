I'll not bother you with long descriptions of what Sharper is,
you downloaded it so you know what it is!


Note on Sharper.ini:
====================
The ini file is read once, when the program starts.
The commands in the ini file are executed EXACTLY the same way
as commands entered by hand, so if you like you can put the
"quit" command in the ini file and Sharper will exit =).


Move pieces:
============
Make moves by entering the source and destination squares, example "e2e4".
Promotion is marked by adding a 'q', 'n', 'r' or 'b' at the end of the command.


Commands:
=========
?
Move now. Forces the engine to move now. This command also stops any pondering.

bench
Runs a speed test on some positions that are hard coded into the program,
used for testing performance of the engine.

bk
Have a look at what info the opening book has on the current position.
In case the position is not in the book, this command will do nothing.

correct
Runs a "perfthash" test on some selected positions, to verify that the move
generator is correct, used for finding bugs.

divide
Performs a "perfthash" test on each legal move in the current position, great
for locating bugs, not very useful for anything else.

easy
Turns pondering (thinking on opponents time) off.

force
Set the engine in force mode, the engine will not respond to your moves by
thinking and making own moves.

go
Tell the engine to leave force mode, and start thinking now.

hard
Turns pondering (thinking on opponents time) on.

hashsize
Set the size of the main transposition table. The size is measured in mega bytes.
Default size is 8 MB.

level
Used to set the time control of the game.

logon
Turn command input and output logging on.

logoff
Turn command input and output logging off.

new
Initializes the engine to start position.

perft
Calculate all valid moves to a given depth, this is used to test the speed
of the move generator and to see if the move generator is correct.

perfthash
Calculate all valid moves to a given depth, this is used to test the speed
of the move generator and to see if the move generator is correct. This
commands uses the hashtable to perform the test more efficiently.

perftprint
Print all valid positions to a given depth.

print
Display the board.

printall
Display the board, and some internal board data.

quit
Exit the engine.

savepos
Print the FEN for the current position.

setboard
Set the engine to this FEN position.

sd
Set the maximum search depth, default is 50.

st
Set the number of seconds for the engine to think.
