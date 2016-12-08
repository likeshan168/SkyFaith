;#DropDB SkyFaithEasyTest

#Application SkyFaith.Win
#Application SkyFaith.Web

*FillForm
 User Name = Admin
*Action Log On

#IfDef SkyFaith.Win

*Action About...
 
*Action OK

#EndIf

#IfDef SkyFaith.Web

*Action Choose Theme(Black Glass)

#EndIf
