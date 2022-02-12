import { render, screen } from '@testing-library/react';
import UtilizadorObjetivo from '../src/UtilizadorObjetivo/UtilizadorObjetivo';

test('numberOfTagsUtilizadorObjetivo', () => {
    render(<UtilizadorObjetivo />);
    const linkElement = screen.getByText(/Number of tags/i);
    expect(linkElement).toBeInTheDocument();
});