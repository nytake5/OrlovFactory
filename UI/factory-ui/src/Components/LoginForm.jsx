import React, { Component, useState } from 'react';
//import { AccountService } from '../../API/AccountService';
import MyButton from './UI/Button/MyButton';
import MyInput from './UI/Input/MyInput';
import classes from './Login.module.scss';  

const LoginForm = (props) => {
    const [login, setLogin] = useState('');
    const [password, setPassword] = useState('');

    const LoginUser = async (e) => {
      e.preventDefault()
      const isLogin = false;
      console.log(isLogin)
      if(!isLogin){
        alert("Something that wrong, pls try again!")
      }
      else{
        props.setVisible(false)
      }
      setLogin('');
      setPassword('');
    }

    return (
      <div className={classes.LoginForm}>
        <MyInput 
          value={login} 
          type='text' 
          required
          onChange={e => setLogin(e.target.value)}
          placeholder="Login"
        />
        <MyInput  
          value={password} 
          type='password' 
          required
          onChange={e => setPassword(e.target.value)}
          placeholder="Password"
        />
        <MyButton onClick={LoginUser}>SignIn</MyButton>
      </div>
    )
}

export default LoginForm