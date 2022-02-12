import { render, screen } from '@testing-library/react';
import AcceptOrRefuse from '../src/PedidosDeLigacao/AceitarERejeitar';

test('Accept or Refuse Connection', () => {
    render(<AcceptOrRefuse />);
    const linkElement = screen.getByText(/Accept or Refuse Connection/i);
    expect(linkElement).toBeInTheDocument();
});

