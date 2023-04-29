CC = csc

BIN = Whodunit
SRC = GenTree.cs Program.cs Suspects.cs

all: ${BIN}

${BIN}:
	${CC} ${SRC} -out:${BIN}.exe

run: ${BIN}
	mono ${BIN}.exe

clean:
	${RM} *.exe *.dot

.PHONY: clean
