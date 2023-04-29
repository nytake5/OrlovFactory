import './App.css';
import './Components/LoginForm'
import LoginForm from './Components/LoginForm';
import MyModal from './Components/UI/MyModal/MyModal';
import React, { Component, useState} from 'react';

function App() {

  const [modalLogin, setmodalLogin] = useState(true);


  return (
    <div className="App">
      <MyModal visible={true} setVisible={setmodalLogin}>
        <LoginForm/>
      </MyModal>
    </div>
  );
}

export default App;
