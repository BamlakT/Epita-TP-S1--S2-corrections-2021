CC = ocamlc
SRC = GenTree.ml

BIN = GenTree

all:
	$(CC) $(SRC) -o $(BIN)

clean:
	$(RM) $(BIN) $(SRC:%.ml=%.cmi) $(SRC:%.ml=%.cmo)
