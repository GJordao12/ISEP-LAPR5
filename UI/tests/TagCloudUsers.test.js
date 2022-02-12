import { render, screen } from '@testing-library/react';
import TagsCloud from '../src/Tags/TagsFromUsersTable';

test('Tag Names', () => {
    render(<TagsCloud />);
    const linkElement = screen.getByText(/Tag Names/i);
    expect(linkElement).toBeInTheDocument();
});

test('Occurrences', () => {
    render(<TagsCloud />);
    const linkElement = screen.getByText(/Occurrences/i);
    expect(linkElement).toBeInTheDocument();
});