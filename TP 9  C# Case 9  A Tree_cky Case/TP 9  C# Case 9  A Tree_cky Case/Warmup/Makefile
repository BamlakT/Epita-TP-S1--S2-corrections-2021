CC = csc

BIN = Warmup
SRC = ${BIN}.cs Program.cs

all: ${BIN}

${BIN}:
	${CC} ${SRC} -out:${BIN}.exe

run: ${BIN}
	mono ${BIN}.exe

clean:
	${RM} *.exe

.PHONY: clean
