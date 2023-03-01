import React, { useState, useEffect } from 'react';
import Table from 'react-bootstrap/Table';

const AppApi = () => {
   const [posts, setPosts] = useState([]);
   useEffect(() => {
      fetch('https://jsonplaceholder.typicode.com/posts?_limit=10')
         .then((response) => response.json())
         .then((data) => {
            console.log(data);
            setPosts(data);
         })
         .catch((err) => {
            console.log(err.message);
         });
   }, []);

    return (//posts);
        <div className="posts-container">
            {posts.map((post) => {
                return (
                    <div className="post-card" key={post.userId}>
                        <h2 className="post-title">{post.id}</h2>
                        <p className="post-body">{post.title}</p>
                    </div>
                );
            })}
        </div>
    );
};

export default AppApi;