﻿using Castle.Components.DictionaryAdapter.Xml;
using ChatApp;
using ChatNetWork;

var messageSource = new MessageSource(7000);
ChatServer chatServer = new ChatServer(messageSource);
Task task = chatServer.WorkAsync();
task.Wait();