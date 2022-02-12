import { render, screen } from '@testing-library/react';
import TagsCloud from '../src/Tags/TagsFromUserTable';

test('YOUR TAG CLOUD', () => {
    render(<TagsCloud />);
    const linkElement = screen.getByText(/YOUR TAG CLOUD:/i);
    expect(linkElement).toBeInTheDocument();
});

test('Tag Names', () => {
    render(<TagsCloud />);
    const linkElement = screen.getByText(/Tag Names/i);
    expect(linkElement).toBeInTheDocument();
});