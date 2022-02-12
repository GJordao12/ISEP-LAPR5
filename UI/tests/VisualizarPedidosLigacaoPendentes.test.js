import { render, screen } from '@testing-library/react';
import VisualizarPedidosLigacaoPendentes from '../src/VisualizarPedidosLigacaoPendentes/ScrollableTable';

test('Pending Connection Requests', () => {
    render(<VisualizarPedidosLigacaoPendentes />);
    const linkElement = screen.getByText(/PENDING CONNECTION REQUESTS:/i);
    expect(linkElement).toBeInTheDocument();
});

test('Sender', () => {
    render(<VisualizarPedidosLigacaoPendentes />);
    const linkElement = screen.getByText(/Sender/i);
    expect(linkElement).toBeInTheDocument();
});

test('Text', () => {
    render(<VisualizarPedidosLigacaoPendentes />);
    const linkElement = screen.getByText(/Text/i);
    expect(linkElement).toBeInTheDocument();
});