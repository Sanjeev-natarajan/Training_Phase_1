export interface CartItem {
  cartItemId: number;
  productId: number;
  productName: string;
  price: number;
  quantity: number;
  total: number;
}

export interface CartResponse {
  orderId: number;
  userId: number;
  cartItems: CartItem[];
  totalAmount: number;
}
