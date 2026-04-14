using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Response
{
    public bool isSuccess;
    public string Notification;
    public Account Data;
}

[Serializable]
public class Account
{
    public int Id;
    public string Email;
    public string Password;
    public string Password_Salt;
    public string Name;
    public string Created_at;
    public string Updated_at;
}

[Serializable]
public class Character
{
    public int Id;
    public int Account_id;
    public string Name;
    public int Level;
    public int Exp;
    public string Created_at;
    public string Updated_at;
}

[Serializable]
public class ResponseAccountList
{
    public bool isSuccess;
    public string Notification;
    public List<Account> Data;
}

[Serializable]
public class ResponseCharacterList
{
    public bool isSuccess;
    public string Notification;
    public List<Character> Data;
}

[Serializable]
public class LoginRequest
{
    public string Email;
    public string Password;
}

[Serializable]
public class RegisterRequest
{
    public string Email;
    public string Password;
    public string Name;
}