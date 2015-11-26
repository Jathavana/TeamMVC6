dnvm use 1.0.0-beta7

dnx ef migrations add FirstMigration --context OptionsContext
dnx ef database update --context OptionsContext

