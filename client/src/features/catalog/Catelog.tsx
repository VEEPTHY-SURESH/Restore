import { Grid2, Typography } from "@mui/material";
import ProductList from "./ProductList";
import { useFetchFiltersQuery, useFetchProdctsQuery } from "./catalogApi";
import Filters from "./Filters";
import { useAppDispatch, useAppSelector } from "../../app/store/store";
import AppPagination from "../../app/shared/components/AppPagination";
import { setPageNumber } from "./CatalogSlice";

export default function Catelog() {
  const productParams = useAppSelector((state) => state.catalog);
  const { data, isLoading } = useFetchProdctsQuery(productParams);
  const {data: filtersData, isLoading: filtersLoading} = useFetchFiltersQuery();
  const dispatch = useAppDispatch();

  if (isLoading || !data || !filtersData || filtersLoading) return <div>Loading...</div>;

  return (
    <Grid2 container spacing={4}>
      <Grid2 size={3}>
        <Filters filtersData={filtersData} />
      </Grid2>
      <Grid2 size={9}>
        {data.items && data.items.length > 0 ? (
          <>
            <ProductList products={data.items} />
            <AppPagination
              metadata={data.pagination}
              onPageChange={(page: number) => {
                dispatch(setPageNumber(page))
                window.scrollTo({top: 0, behavior: 'smooth'})
              }}
            />
          </>
        ): (
          <Typography variant="h5">There are no results for this filters</Typography>
        )}
        
      </Grid2>
    </Grid2>
  );
}
