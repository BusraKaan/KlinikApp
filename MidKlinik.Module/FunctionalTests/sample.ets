;#DropDB MidKlinikEasyTest

#Application MidKlinik.Win
#Application MidKlinik.Web
#IfDef MidKlinik.Win

*Action About...
 
*Action OK

#EndIf

#IfDef MidKlinik.Web
!Action Navigation(My Details)
#EndIf
