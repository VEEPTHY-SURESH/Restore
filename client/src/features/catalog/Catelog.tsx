
import ProductList from "./ProductList";
import { useFetchProdctsQuery } from "./catalogApi";

export default function Catelog() {
  
  const {data, isLoading} = useFetchProdctsQuery();

  if(isLoading || !data) return <div>Loading...</div>

  return (
    <> 
      <ProductList products={data} />
    </>
  )
}