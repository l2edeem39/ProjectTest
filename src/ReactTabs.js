import React, { useState, useEffect, useRef } from 'react';
import ConnectApi from './ConnectApi';
import Table from 'react-bootstrap/Table';
import {
  MDBTabs,
  MDBTabsItem,
  MDBTabsLink,
  MDBTabsContent,
  MDBTabsPane
} from 'mdb-react-ui-kit';

export default function App() {
  const [fillActive, setFillActive] = useState('tab1');

  const handleFillClick = (value : String) => {
    if (value === fillActive) {
      return;
    }

    setFillActive(value);
  };

  const [card, setCard] = useState();
  const inputCard = useRef();

  const handleChange = () => {
    const cardValue = inputCard.current.value
      .replace(/\D/g, '')
      .match(/(\d{0,4})(\d{0,4})(\d{0,4})(\d{0,4})/);
    inputCard.current.value = !cardValue[2]
      ? cardValue[1]
      : `${cardValue[1]}-${cardValue[2]}${`${
          cardValue[3] ? `-${cardValue[3]}` : ''
        }`}${`${cardValue[4] ? `-${cardValue[4]}` : ''}`}`;
    const numbers = inputCard.current.value.replace(/(\D)/g, '');
    setCard(numbers);
  };



  useEffect(() => {
    handleChange();
  }, [card]);


  const [hideLightbox, setHideLightbox] = useState(false);

    const [posts, setPosts] = useState([]);
    useEffect(() => {
       fetch('https://services.odata.org/V4/Northwind/Northwind.svc/Orders')
          .then((response) => response.json())
          .then((data) => {
             console.log(data.value);
             setPosts(data.value);
          })
          .catch((err) => {
             console.log(err.message);
          });
    }, []);

  return (
    <>
    <h2>Download PDF</h2><br />
      <MDBTabs fill className='mb-3'>
        <MDBTabsItem>
          <MDBTabsLink onClick={() => handleFillClick('tab1')} active={fillActive === 'tab1'}>
            CMI
          </MDBTabsLink>
        </MDBTabsItem>
        <MDBTabsItem>
          <MDBTabsLink onClick={() => handleFillClick('tab2')} active={fillActive === 'tab2'}>
            VMI
          </MDBTabsLink>
        </MDBTabsItem>
      </MDBTabs>

      <MDBTabsContent>
        <MDBTabsPane show={fillActive === 'tab1'}>
          <div class="input-group mb-3">
            <input type="text" class="form-control" placeholder="____-____-____-____" aria-label="yy/mm/dd" aria-describedby="button-addon2" ref={inputCard} onChange={handleChange}/>
            <button class="btn btn-outline-secondary" type="button" id="button-addon2" onClick={() => setHideLightbox(true)}>Search</button>
            <button class="btn btn-outline-secondary" type="button" id="button-addon2" onClick={() => setHideLightbox(false)}>Clear</button>
          </div>
          <div className={`lightbox ${hideLightbox ? "" : "d-none"}`}>
        <Table striped bordered hover>
          <thead>
            <tr style={{textAlign: "center"}}>
              <th>OrderID</th>
              <th>CustomerID</th>
              <th>ShipAddress</th>
              <th>ShipCity</th>
              <th>ShipCountry</th>
              <th>ShipName</th>
            </tr>
          </thead>
          <tbody>
            {posts.map((post) => {
                  return (
                      <tr>
                        <td>{post.OrderID}</td>
                        <td>{post.CustomerID}</td>
                        <td>{post.ShipAddress}</td>
                        <td>{post.ShipCity}</td>
                        <td>{post.ShipCountry}</td>
                        <td>{post.ShipName}</td>
                      </tr>
                  );
              })}
          </tbody>
        </Table>
      </div>
        </MDBTabsPane>
        <MDBTabsPane show={fillActive === 'tab2'}>
          <div class="input-group mb-3">
            <input type="text" class="form-control" placeholder="YYYY/MM/DD" aria-label="yy/mm/dd" aria-describedby="button-addon2" />
            <button class="btn btn-outline-secondary" type="button" id="button-addon2">Search</button>
            
          </div>
        </MDBTabsPane>
      </MDBTabsContent>
          <button className='d-none' onClick={() => setHideLightbox(true)}>Show Lightbox</button>
          <button className='d-none' onClick={() => setHideLightbox(false)}>hide Lightbox</button>
          <input className='d-none' value={hideLightbox}/>
    </>
  );
}