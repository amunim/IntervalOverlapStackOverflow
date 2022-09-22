IntervalOverlapStackOverflow

Stack overflow question was asked for a way to insert items into batches with around a million records, since the question was deleted I decided to post my answer here ;)


The basic idea was to follow FIFO to find out how many items could be inserted into a batch, if 200 were bought then 300 and 400, for a batch of 500 it should show 200 and 300 in the same order. This of course can be done with a Queue, but OP specifically asked for LINQ.

for data
Items:

Item | Qty
---- | ---
foo  | 300
bar  | 200
xxx  | 400
yyy  | 800
zzz  | 100

Batches:

Batch | Qty
----- | ---
A     | 500
B     | 200
C     | 400

Output should be:

Batch | Item | Qty
----- | ---- | ---
A     | foo  | 300
A     | bar  | 200
B     | xxx  | 200
C     | xxx  | 200
C     | yyy  | 200
