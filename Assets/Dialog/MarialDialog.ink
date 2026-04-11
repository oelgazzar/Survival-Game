VAR firstTime = true
VAR questState = "NotStarted"

#quest:findCrate

=== entry ===
    {questState:
        - "NotStarted":
            -> questNotStarted
        - "InProgress":
            -> questInProgress
        - "Completed" :
            -> questCompleted
        - "Declined" :
            -> questDeclined
    }
-> DONE


=== questNotStarted ===
    {firstTime: 
        You look new around here. Supplies are hard to find lately.
    - else:
    	Hello, again. How can I help you?
    }
	    + [Any work available?]
	        -> quest
	    + [Just passing by]
	        -> exit

=== quest ===
	I lost a supply crate near the old tower.
	+ [I'll find it]
	    -> accept
	+ [Sounds dangerous]
	    -> decline
    	    
=== accept ===
    #event:startQuest_findCrate
	Thanks. Come back when you find it.
	+ [Leave]
	-> END
	
=== decline ===
    #event:declineQuest_findCrate
    Suit yourself.
    + [Leave]
    -> END

=== exit ===
	Stay safe out there.
	+ [Leave]
    -> END

// -------------------------------

=== questInProgress ===
	Have you found the supply crate yet?
	+ [Not yet]
	    -> keepLooking

=== keepLooking ===
	Keep looking. It's important.
	+ [OK]
    -> END
	
// --------------------------------

=== questCompleted ===
    Thank you for finding the crate.
    + [Leave]
    -> END

// --------------------------------

=== questDeclined ===
    Did you change your mind?
    + [yes]
        -> accept
    + [still no]
        -> decline
    -> DONE
	
-> END