import type { Product } from "../../app/models/product";
import ProductList from "./ProductList";

type Props = {
  products: Product[];
}

export default function Catelog({products}: Props) {
  return (
    <> 
      <ProductList products={products} />
    </>
  )
}