import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import InputMask from './InputMask';
import ConnectApi from './ConnectApi';
import Axios from './Axios';

const x = 5;
let text = "Goodbye";
if (x < 10) {
  text = "Hello";
}

const myElement = (
<div>
    <h1 className='myclass'>I am a paragraph.</h1>
    <input type="Text" value={text}/>
</div>
);

const y = 5;

const myElement2 = <h1>{(y) < 10 ? "Hello" : "Goodbye"}</h1>;

function Car() {
  return <h2>Hi, I am a Car!</h2>;
}

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(myElement);
//root.render(myElement2);


//class Car1 extends React.Component {
  //constructor() {
    //super();
    //this.state = {color: "red"};
  //}
  //render() {
    //return <h2>I am a <span className={this.state.color}>{this.state.color}</span> Car!</h2>;
  //}
//}

//const root1 = ReactDOM.createRoot(document.getElementById('root'));
//root1.render(<Car1 />);

//root.render(<App/>);
//root.render(<ConnectApi/>);
root.render(<App/>);