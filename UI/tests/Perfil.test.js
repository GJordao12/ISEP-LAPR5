import { render, screen } from '@testing-library/react';
import Perfil from '../src/Perfil/Perfil';

test('Edit profile', () => {
    render(<Perfil />);
    const linkElement = screen.getByText(/EDIT PROFILE/i);
    expect(linkElement).toBeInTheDocument();
});

