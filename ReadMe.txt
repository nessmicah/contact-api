*** Database Connection ***

Update appsettings.json and appsettings.Development.json - Replace DefaultConnection 'Filename' with your local file path to Contacts.db.

If you are using MacOS, do not remove 'mode=Exclusive' from your DefaultConnection. This prevents LiteDB from through 'FileStream.Lock is not supported' error in v4 or below. Developer will be removing FileStream in v5.
