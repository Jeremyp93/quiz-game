import { useEffect, useRef, RefObject } from 'react';

interface UseAutoTextSizeOptions {
  minFontSize?: number;
  maxFontSize?: number;
  step?: number;
}

/**
 * Custom hook that automatically adjusts font size to fit text within a container
 * Uses ResizeObserver to reactively update when content or container size changes
 */
export function useAutoTextSize(
  containerRef: RefObject<HTMLElement>,
  options: UseAutoTextSizeOptions = {}
): RefObject<HTMLDivElement> {
  const {
    minFontSize = 12,
    maxFontSize = 100,
    step = 1,
  } = options;

  const textRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    const container = containerRef.current;
    const textElement = textRef.current;

    if (!container || !textElement) return;

    const adjustFontSize = () => {
      // Get the parent element that contains this text (question-lang div)
      const parent = textElement.parentElement;
      if (!parent) return;

      const containerWidth = container.clientWidth;
      const containerHeight = container.clientHeight;
      const containerPadding = parseFloat(getComputedStyle(container).paddingTop) + parseFloat(getComputedStyle(container).paddingBottom);

      // Find the label within the parent (question-lang div)
      // CSS modules add hash to class names, so we need to find by partial class match
      const langLabel = Array.from(parent.children).find((el): el is HTMLElement =>
        el instanceof HTMLElement && el.tagName === 'SPAN'
      ) as HTMLElement | null;

      // Find the divider within the container (look for div between the two question-lang divs)
      const divider = Array.from(container.children).find((el): el is HTMLElement =>
        el instanceof HTMLElement && el.className.includes('question-divider')
      ) as HTMLElement | null;

      // Calculate heights of fixed elements
      const labelHeight = langLabel ? langLabel.offsetHeight : 0;
      const labelGap = langLabel ? parseFloat(getComputedStyle(parent).gap || '0') : 0;
      const dividerHeight = divider ? divider.offsetHeight : 0;
      const dividerMargin = divider ? parseFloat(getComputedStyle(divider).marginTop) + parseFloat(getComputedStyle(divider).marginBottom) : 0;

      // Available height for text (container height divided by 2 sections, minus all fixed elements)
      // Each section has: label + gap + text, and there's a divider between them
      const totalFixedHeight = dividerHeight + dividerMargin + (labelHeight * 2) + (labelGap * 2) + containerPadding;
      const availableHeight = Math.max(50, (containerHeight - totalFixedHeight) / 2); // Minimum 50px

      // Debug logging
      console.log('useAutoTextSize:', {
        containerHeight,
        containerPadding,
        labelHeight,
        labelGap,
        dividerHeight,
        dividerMargin,
        totalFixedHeight,
        availableHeight,
        containerWidth
      });

      // Binary search for optimal font size
      let low = minFontSize;
      let high = maxFontSize;
      let bestSize = minFontSize;

      while (low <= high) {
        const mid = Math.floor((low + high) / 2);
        textElement.style.fontSize = `${mid}px`;

        const textHeight = textElement.scrollHeight;
        const textWidth = textElement.scrollWidth;

        // Check if text fits within available space
        if (textHeight <= availableHeight && textWidth <= containerWidth) {
          bestSize = mid;
          low = mid + step;
        } else {
          high = mid - step;
        }
      }

      textElement.style.fontSize = `${bestSize}px`;
      console.log('Final font size:', bestSize, 'px');
    };

    // Use requestAnimationFrame to ensure layout is complete
    const rafId = requestAnimationFrame(() => {
      // Add small delay after RAF to ensure everything is painted
      setTimeout(() => {
        adjustFontSize();
      }, 50);
    });

    // Create ResizeObserver to watch for size changes
    const resizeObserver = new ResizeObserver(() => {
      requestAnimationFrame(() => {
        adjustFontSize();
      });
    });

    // Observe both container and parent element
    resizeObserver.observe(container);
    if (textElement.parentElement) {
      resizeObserver.observe(textElement.parentElement);
    }

    // Create MutationObserver to watch for content changes
    const mutationObserver = new MutationObserver(() => {
      requestAnimationFrame(() => {
        adjustFontSize();
      });
    });

    mutationObserver.observe(textElement, {
      childList: true,
      subtree: true,
      characterData: true,
    });

    // Cleanup
    return () => {
      cancelAnimationFrame(rafId);
      resizeObserver.disconnect();
      mutationObserver.disconnect();
    };
  }, [containerRef, minFontSize, maxFontSize, step]);

  return textRef;
}
