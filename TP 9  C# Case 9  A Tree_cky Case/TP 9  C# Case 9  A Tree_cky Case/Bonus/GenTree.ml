type binTree =
  | Person of string * int
  | GenTree of binTree * binTree * binTree
  | Empty
;;

[@@@warning "-8"] (* avoiding warning that won't happen since file is always
                     valid (predicate) *)
(**
 * \brief Builds the tree corresponding to a valid .gen file
 * **)
let treeFromFile path =
  (* Utility, reads the first line of a file and splits it on '-' *)
  let read_line file =
  let in_chan = open_in file in
  let s = input_line in_chan in
    close_in in_chan;
    String.split_on_char '-' s
  in

  (* List of suspects on the line *)
  let lines = read_line path in

  (* Building the tree  using previously defined list *)
  let rec build x =
    if x >= List.length lines then
         Empty
    else
      let names = String.split_on_char ' ' (List.nth lines x) in
      let name::names = names in
      let suspicion::names = names in
      GenTree (Person (name, int_of_string suspicion),
               build (2 * x + 1),
               build (2 * x + 2))
  in
  build 0;
;;
[@@@warning "+8"]

let printTree tree =
  let rec printer t =
    match t with
      Empty -> ()
    | GenTree (Person (name, _), Empty, right) -> print_string name;
      printer right
    | GenTree (Person (name, _), left, Empty) -> print_string name;
      printer left
    | GenTree (Person (name, _), left, right) -> print_string name;
      print_string " ("; printer left; print_string " "; printer right;
      print_string ")";
    | _ -> failwith "printTree: How did you get there ?"
  in
  printer tree;
  print_newline
;;

let changeName tree oldName newName = 
  let rec change t =
    match t with
      Empty -> Empty
      | GenTree (Person (name, sus), left, right)
        when String.equal name oldName ->
        GenTree (Person (newName, sus), left, right)
      | GenTree (Person (name, sus), left, right) ->
        GenTree (Person (name, sus), change left, change right)
      | _ -> failwith "changeName: How did  you get there ?"
  in
  change tree
;;

let findPath t name =
  let rec finder tree l =
    match tree with
      Empty -> []
    | GenTree (Person (n, _), _, _) when String.equal n name-> l
    | GenTree (Person (n, _), left, right) ->
      List.rev_append (finder left (n::l)) (finder right (n::l))
    | _ -> failwith "findPath: How did you get there ?"
  in
  List.rev (finder t [])
;;
