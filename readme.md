(scroll to the bottom for latest notes)

Top Game is a deterministic card game, best played late at night while under the influence.

It involves no skill at all, but can nonetheless be surprisingly dramatic and engaging.

This software was written because I and my partner spotted various patterns emerging after playing the game several hundred (if not thousand) times. I wanted a graphical representation of the game, so that you could watch the face cards move between players and see patterns in an accessible format. I also wanted to know whether an infinite game was possible (this question has still not been answered).

The number of possible games is 52!/(36!x(4!^4)) = 653,534,134,886,878,200,000. So brute force is not an option. :)

!! This is a great example of how NOT to write software. It was written in a hurry, and even I (the author) struggle to understand what the hell is going on when looking at the code. Also the graphics are not at all efficient - and a bit buggy.

However it will hopefully now form a great basis for a massive TDD / webapp / refactoring project - I hope to rewrite the solution as a web app, using a fully-fledged TDD approach.

Note that work on the refactor has already started, but is still at an early stage - I am currently trying to create a "golden master" so that I can add tests before refactoring, and ensure that each refactoring step does not break any existing code.

**When I started the Golden Master work, I made a poor data-storage choice. Next time I work on this, that'll be the first thing I change. :)

See Helpfile.docx (in the Docs folder) for a detailed description of the game, along with instructions on how to use this software and play the game.

15/10/14: I've added the exe for an earlier more polished version of the software - so you don't get rogue buttons appearing on the screen. I've put it in a "Release" folder.
