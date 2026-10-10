import React from 'react';
import { Link, useParams } from 'react-router-dom';

function DishDetailPage() {
  const { id } = useParams();

  return (
    <div>
      <p>Showing details for dish #{id}</p>
      <Link to="/">Back to menu</Link>
    </div>
  );
}

export default DishDetailPage;
