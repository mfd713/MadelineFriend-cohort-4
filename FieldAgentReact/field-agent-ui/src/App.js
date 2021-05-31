import './App.css';
import React from 'react';
import MainView from './Components/MainView';
import Nav from './Components/Nav';

function App() {
  return (
    <div className="App">
      <Nav></Nav>
      <MainView></MainView>
    </div>
  );
}

export default App;
