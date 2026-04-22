->main
===main===
Once upon a time...
   Do you hear me? 
    *[Yes]
        Thats great!
        ->second
    *[Weird]
        -So what.
         ->second
===second===
The second line appeared
    Still hear me?
        **[Yes...]
            Thats great!
                ->third
        **[NO!]
             So weird
                ->third
===third===
I dont think that you heard me.

    -> END
