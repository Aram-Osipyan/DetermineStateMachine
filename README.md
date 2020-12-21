# DetermineStateMachine
algorithm for determining a finite state machine C#

# The [algoritm](https://github.com/Aram-Osipyan/DetermineStateMachine/blob/master/DetermineStateMachine/Program.cs)  

# An example of the input file :

```
;0;1
s;p;c0,m,c
p;p;m,c0
m;m;p
c;c;m2
m2;m2;c
c0;c1;
c1;;c
```
# An example of the ouput :
```
f(s,0) = Q0
f(s,1) = Q1
f(Q0,1) = Q2
f(Q1,0) = Q3
f(Q1,1) = Q4
f(Q2,0) = Q5
f(Q3,0) = Q6
f(Q3,1) = Q7
f(Q5,0) = Q8
f(Q5,1) = Q9
f(Q7,1) = Q10
f(Q9,1) = Q11
f(Q10,0) = Q12
f(Q11,0) = Q13
f(Q12,0) = Q14
f(Q13,0) = Q15
```

# An example of the program launching :

```
DetermineStateMachine.exe input.txt
```
