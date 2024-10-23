create DATABASE  Accounts
go
use Accounts
go
create table Accounts(
AccountID [int] NOT NULL IDENTITY(1,1) PRIMARY KEY,
FIO [varchar](max) NULL,
Login [varchar](max) NULL,
Password [varchar](max) NULL
)
go
insert into Accounts values('FIO','adm', 'A665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3')
/*пользователь с логином adm и паролем 123 */