import { render, screen } from '@testing-library/react';
import RelationshipStrength from '../src/Connection&RelationshipStrength/ScrollableTable';

test('CONNECTION AND RELATIONSHIP STRENGTHS', () => {
    render(<RelationshipStrength />);
    const linkElement = screen.getByText(/CONNECTION AND RELATIONSHIP STRENGTHS:/i);
    expect(linkElement).toBeInTheDocument();
});

test('User you are connected to', () => {
    render(<RelationshipStrength />);
    const linkElement = screen.getByText(/User you are connected to/i);
    expect(linkElement).toBeInTheDocument();
});

test('Connection Strength', () => {
    render(<RelationshipStrength />);
    const linkElement = screen.getByText(/Connection Strength/i);
    expect(linkElement).toBeInTheDocument();
});